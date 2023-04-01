using Cards.API.Data;
using Cards.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cards.API.Controllers
{
    //ng analytics disable --global
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : Controller
    {
        private readonly cardsDbContext cardsDbContext;

        public CardController(cardsDbContext cardsDbContext)
        {
            this.cardsDbContext = cardsDbContext;
        }
        //get all cards
        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            var cards = await cardsDbContext.Cards.ToListAsync();
            return Ok(cards);
        }
        //get single cards
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("Getcard")]

        public async Task<IActionResult> Getcard([FromRoute] Guid id)
        {
            var card = await cardsDbContext.Cards.FirstOrDefaultAsync( x=> x.Id ==id);
            if(card != null)
            {
                return Ok(card);
            }
            return NotFound("Card not Found");
        }

        //add single cards
        [HttpPost]

        public async Task<IActionResult> AddCard([FromBody] Card card)
        {
          card.Id = Guid.NewGuid();


          await cardsDbContext.Cards.AddAsync(card);
            await cardsDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Getcard), new { id = card.Id },card);
        }

        //updating the card
        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateCard([FromRoute] Guid id, [FromBody] Card card)
        {
            var existingCard = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCard != null)
            {
                existingCard.CardHolderNumber = card.CardHolderNumber;
                existingCard.CardNumber = card.CardNumber;
                existingCard.Expirymonth = card.Expirymonth;
                existingCard.Expiryyear = card.Expiryyear;
                existingCard.CVC =card.CVC;
                await cardsDbContext.SaveChangesAsync();
                return Ok(existingCard);

            }
            return NotFound("Card not Found");
        }


        //deleting the card details
        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteCard([FromRoute] Guid id)
        {
            var existingCard = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCard != null)
            {
                cardsDbContext.Remove(existingCard);
                await cardsDbContext.SaveChangesAsync();
                return Ok(existingCard);

            }
            return NotFound("Card not Found");
        }
    } 
}
