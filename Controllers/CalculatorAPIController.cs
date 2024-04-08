using CalculatorWebAPI.DTO;
using CalculatorWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CalculatorWebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class CalculatorAPIController : ControllerBase
    {
        
        // POST api/solve
        [HttpPost("solve")]
        public IActionResult Solve([FromBody] ExerciseDto exercise)
        {
            try
            {
                //Get DTO class and change it to Model class
                Exercise e = new Exercise
                {
                    FirstVal = exercise.FirstVal,
                    SecondVal = exercise.SecondVal,
                    Op = exercise.Op
                };
                
                //Call model operation
                ExerciseResult res = e.Solve();

                //Change back the result to a DTO class
                ExerciseResultDto ret = new ExerciseResultDto
                {
                    Exercise = exercise,
                    Success = res.Success,
                    Result = res.Result
                };

                return Ok(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // Get /api/solve?first=20&op=%2B&second=9
        //%2B stands for +
        //in other operations just put the character (because + is special character
        [HttpGet("solve")]
        public IActionResult Solve([FromQuery] int first, [FromQuery] char op, [FromQuery] int second)
        {
            try
            {
                //Build the model exercise class
                Exercise e = new Exercise
                {
                    FirstVal = first,
                    SecondVal = second,
                    Op = op
                };

                //Call model operation
                ExerciseResult res = e.Solve();

                //Change back the result to a DTO class
                ExerciseDto exercise = new ExerciseDto
                {
                    FirstVal = e.FirstVal,
                    SecondVal = e.SecondVal,
                    Op = e.Op
                };
                ExerciseResultDto ret = new ExerciseResultDto
                {
                    Exercise = exercise,
                    Success = res.Success,
                    Result = res.Result
                };

                return Ok(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        


    }
}

