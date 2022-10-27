using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyHomeGroup_VillaApi.Data;
using MyHomeGroup_VillaApi.Logging;
using MyHomeGroup_VillaApi.Models;
using MyHomeGroup_VillaApi.Models.Dto;
using MyHomeGroup_VillaApi.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MyHomeGroup_VillaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        private readonly ILogging _logger;

        private readonly ApplicationDbContext _dbContext;
        private readonly IVillaRepository _dbVillaUnit;
        private readonly IMapper _mapper;

       // private readonly VillaRepository _dbVilla;

        public VillaApiController(ILogging logger, IMapper mapper, ApplicationDbContext dbContext,IVillaRepository dbVillaUnit)
        {
            _logger = logger;
            //_dbContext = dbContext;
            _mapper = mapper;
         //   _dbContext = dbContext;
            _dbVillaUnit = dbVillaUnit;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            IEnumerable<Villa> villaList = await _dbVillaUnit.GetVillasAsync();
          //  IEnumerable<Villa> obVillas = await _dbContext.Villas.Include(x => x.Amenties).ToListAsync();
            return Ok(_mapper.Map<List<VillaDTO>>(villaList));

            // return vList;
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (id <= 0)
            {
                _logger.Log("Get Villa Error due to Id: " + id, "Error");
                return BadRequest();
            }
            // Villa ob = await _dbContext.Villas.Include(x=>x.Amenties).FirstOrDefaultAsync(x => x.Id == id); ;
            Villa ob = await _dbVillaUnit.GetVillaAsync(x=>x.Id==id);
          //  Villa ob = await _dbVilla.GetAsync(x => x.Id == id);
            if (ob == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VillaDTO>(ob));
        }

        [HttpPost]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] VillaCreatedDTO obNewDTOVilla)
        {
            if(_dbVillaUnit.GetVillaAsync(x=>x.Name.ToLower()== obNewDTOVilla.Name.ToLower())!=null)
            //  if (await _dbContext.Villas.FirstOrDefaultAsync(x => x.Name.Trim().ToLower() == obNewDTOVilla.Name.Trim().ToLower()) != null)
           // if (await _dbVilla.GetAsync(x => x.Name.Trim().ToLower() == obNewDTOVilla.Name.Trim().ToLower()) != null)
            {
                ModelState.AddModelError("Custom", "Similar Villa Name already exists.");
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid)
            {


                if (obNewDTOVilla == null)
                {
                    return BadRequest(obNewDTOVilla);
                }

                Villa model = _mapper.Map<Villa>(obNewDTOVilla);

                //Villa model = new()
                //{

                //    Details = obNewDTOVilla.Details,
                //    Name = obNewDTOVilla.Name,
                //    Occupancy = obNewDTOVilla.Occupancy,
                //    PriceRangeStarts = obNewDTOVilla.PriceRangeStarts,
                //    Sqft = obNewDTOVilla.Sqft,
                //    ImageUrl = obNewDTOVilla.ImageUrl,

                //   };
                    model.Amenties = new()
                    {
                    Id = obNewDTOVilla.Amenties.Id,
                    AmphiTheater = obNewDTOVilla.Amenties.AmphiTheater,
                    BasketballCourt = obNewDTOVilla.Amenties.BasketballCourt,
                    ChildrensPlayArea = obNewDTOVilla.Amenties.ChildrensPlayArea,
                    ClubHouseWithWater = obNewDTOVilla.Amenties.ClubHouseWithWater,
                    CricketNets = obNewDTOVilla.Amenties.CricketNets,
                    Duplex = obNewDTOVilla.Amenties.Duplex,
                    TennisCourt = obNewDTOVilla.Amenties.TennisCourt,
                    Triplex = obNewDTOVilla.Amenties.Triplex,
                    JaggingCycleTrack = obNewDTOVilla.Amenties.JaggingCycleTrack,
                    FitnessStation = obNewDTOVilla.Amenties.FitnessStation,
                    PartyLawn = obNewDTOVilla.Amenties.PartyLawn,
                    SkatingRank = obNewDTOVilla.Amenties.SkatingRank,
                    SwimmingPool = obNewDTOVilla.Amenties.SwimmingPool,
                    YogaMeditationLawn = obNewDTOVilla.Amenties.YogaMeditationLawn,
                    DogsPark = obNewDTOVilla.Amenties.DogsPark

                };
             //   await _dbVilla.CreateAsync(model);
                _logger.Log("Created new villa with Id : " + model.Id, "Info");
                //   return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
               await _dbVillaUnit.CreateAsync(model);
              //  await _dbContext.Villas.AddAsync(model);
              //  await _dbContext.SaveChangesAsync();



              //  return Ok(obNewDTOVilla);

                return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public async Task<ActionResult> DeleteVilla(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            // var objVilla =await _dbContext.Villas.FirstOrDefaultAsync(x => x.Id == id);
            var objVilla = await _dbVillaUnit.GetVillaAsync(x => x.Id == id);
           // var objVilla = await _dbVilla.GetAsync(x => x.Id == id);
            if (objVilla == null)
            {
                return NotFound();
            }
            // await _dbVilla.RemoveAsync(objVilla);

            _dbContext.Remove(objVilla);
            await _dbContext.SaveChangesAsync();
            _logger.Log("Deleted villa corresponding to id : " + id, "Info");
            return NoContent();
        }

        [HttpPut( Name = "UpdateVilla")]
        public async Task<ActionResult> UpdateVilla(int id, [FromBody] VillaUpdatedDTO objUpdatVillaDTO)
        {
            if (objUpdatVillaDTO == null || id != objUpdatVillaDTO.Id)
            {
                return BadRequest();
            }

           Villa model = _mapper.Map<Villa>(objUpdatVillaDTO);
            //Villa model = new()
            //{
            //    Id = objUpdatVillaDTO.Id,
            //    Name = objUpdatVillaDTO.Name,
            //    ImageUrl = objUpdatVillaDTO.ImageUrl,


            //    Occupancy = objUpdatVillaDTO.Occupancy,
            //    PriceRangeStarts = objUpdatVillaDTO.PriceRangeStarts,
            //    Sqft = objUpdatVillaDTO.Sqft,
            //};
            model.Amenties = new()
            {
                Id = objUpdatVillaDTO.Amenties.Id,
                AmphiTheater = objUpdatVillaDTO.Amenties.AmphiTheater,
                BasketballCourt = objUpdatVillaDTO.Amenties.BasketballCourt,
                ChildrensPlayArea = objUpdatVillaDTO.Amenties.ChildrensPlayArea,
                ClubHouseWithWater = objUpdatVillaDTO.Amenties.ClubHouseWithWater,
                CricketNets = objUpdatVillaDTO.Amenties.CricketNets,
                Duplex = objUpdatVillaDTO.Amenties.Duplex,
                TennisCourt = objUpdatVillaDTO.Amenties.TennisCourt,
                Triplex = objUpdatVillaDTO.Amenties.Triplex,
                JaggingCycleTrack = objUpdatVillaDTO.Amenties.JaggingCycleTrack,
                FitnessStation = objUpdatVillaDTO.Amenties.FitnessStation,
                PartyLawn = objUpdatVillaDTO.Amenties.PartyLawn,
                SkatingRank = objUpdatVillaDTO.Amenties.SkatingRank,
                SwimmingPool = objUpdatVillaDTO.Amenties.SwimmingPool,
                YogaMeditationLawn = objUpdatVillaDTO.Amenties.YogaMeditationLawn,
                DogsPark = objUpdatVillaDTO.Amenties.DogsPark

            };
            //  await _dbVilla.UpdateAsync(model);

            // _dbContext.Villas.Update(model);
            await _dbVillaUnit.UpdateAsync(model);
          //  await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        //[HttpPatch("{id:int}", Name = "PatchVilla")]

        //public ActionResult PatchVilla(int id, JsonPatchDocument<VillaDTO> objPatchVDTO)
        //{
        //    if (objPatchVDTO == null || id == 0)
        //    {
        //        return BadRequest();
        //    }

        //    var res = _dbContext.Villas.FirstOrDefault(x => x.Id == id);
        //    if (res == null)
        //    {
        //        return BadRequest();
        //    }
        //    VillaDTO villaDto = new VillaDTO()
        //    {
        //        Id = res.Id,
        //        Details = res.Details,
        //       // Amenties = (Models.Dto.Amenties)res.Amenties,
        //        ImageUrl = res.ImageUrl,
        //        Name = res.Name,
        //        Occupancy = res.Occupancy,
        //        PriceRangeStarts = res.PriceRangeStarts,
        //        Sqft = res.Sqft

        //    };

        //    objPatchVDTO.ApplyTo(villaDto, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);
        //    Villa model = new Villa()
        //    {
        //        Id = villaDto.Id,
        //        Name = villaDto.Name,
        //        Details = villaDto.Details,
        //        ImageUrl = villaDto.ImageUrl,
        //        Occupancy = villaDto.Occupancy,
        //        Amenties = (Models.Amenties)villaDto.Amenties,
        //        PriceRangeStarts = villaDto.PriceRangeStarts,
        //        Sqft = villaDto.Sqft,

        //    };
        //    _dbContext.Villas.Update(model);
        //    return NoContent();
        //}


    }
}
