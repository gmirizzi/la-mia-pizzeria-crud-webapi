using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        [HttpPost]
        public IActionResult PostMessage([FromBody] Message message)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            else
            {
                using (PizzeriaContext db = new PizzeriaContext())
                {
                    db.Messages.Add(message);
                    db.SaveChanges();
                    return Ok();
                }
            }
        }
    }
}
