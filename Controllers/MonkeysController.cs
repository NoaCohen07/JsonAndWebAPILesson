using CalculatorWebAPI.DTO;
using CalculatorWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CalculatorWebAPI.Controllers
{
    [Route("monkey")]
    [ApiController]

    public class MonkeysController : Controller
    {
         static MonkeyList list = new MonkeyList();
        [HttpGet("ReadAllMonkeys")]
        public IActionResult ReadAllMonkeys()
        {
            MonkeyListDto mon = new MonkeyListDto();
           
            mon.Monkeys = new List<MonkeyDto>();
            foreach(Monkey m in list.Monkeys)
            {
                mon.Monkeys.Add(new MonkeyDto()
                {
                    Name = m.Name,
                    Location = m.Location,
                    Details = m.Details,
                    ImageUrl =m.ImageUrl,
                    IsFavorite =m.IsFavorite
                });
            }
            return Ok(mon);
        }



        [HttpGet("ReadMonkey")]
        public IActionResult ReadMonkey([FromQuery] string monkeyName)
        {
            try
            {
               // MonkeyList List = new MonkeyList();
                foreach(Monkey m in list.Monkeys)
                {
                    if (m.Name == monkeyName)
                    {
                        MonkeyDto monkeyDto = new MonkeyDto()
                        {
                            Name = m.Name,
                            Location=m.Location,
                            Details=m.Details,
                            ImageUrl=m.ImageUrl,
                            IsFavorite=m.IsFavorite
                        };
                        return Ok(monkeyDto);
                    }
                }
                NotFoundResult result = new NotFoundResult();
                return result;
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddMonkey")]
        public IActionResult AddMonkey([FromBody] MonkeyDto monkey)
        {
            try
            {
                bool isNotOk = false;
                foreach (Monkey m in list.Monkeys)
                {
                    if (m.Name == monkey.Name)
                    {
                        isNotOk = true;
                    }
                }
                if (!isNotOk)
                {
                    Monkey m1 = new Monkey
                    {
                        Name = monkey.Name,
                        Location = monkey.Location,
                        Details = monkey.Details,
                        ImageUrl = monkey.ImageUrl,
                        IsFavorite = monkey.IsFavorite
                    };
                    list.Monkeys.Add(m1);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }        
    }
}
