using api.Data;
using api.DTOs.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/stock")]
//the following controller will upload everything from controller class
[ApiController]
//We specified this class as a Controller that was what we did first because it is easy to modify that way then we are going to add attributes
public class StockController : ControllerBase
{
    //Güvenlik açısından _context değişkenini private tanımladık her sınıf veritabanına erişemesin
    private readonly ApplicationDbContext _context;
    public StockController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        //We used .ToList method to deferred execution it is going to make all sql things for itself
        //.Select kullanmadan toStockDto a atama yapamayız .Select() ToStockDto a immutable bir list geri döndürür
        var stocks = _context.Stock.ToList()
            .Select(s => s.ToStockDto());
        return Ok(stocks);
        //we return everything on the datbase
    }
    
    //Her seferiinde tek kayıt almak için böyle tanımladık
    [HttpGet("{id}")]
    //We need to say exactly what are we returning id i o yüzden ekledik
    public IActionResult GetById([FromRoute] int id)
    {
        var stock = _context.Stock.Find(id); // id e göre kayıt döndürmek için bu metodu kullanıyoruz Find() or FirstOrDefault() 
        if (stock == null)
        {
            return NotFound(); //stok boş olduğunda geri dönüş yapmamız lazım
        }
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
    {
        var stockModel = stockDto.ToStockFromCreateDTO();
        _context.Stock.Add(stockModel);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        //bu kod şu anlama geliyor eğer ,den önceki kısım çalışırsa stockModelin idsini alıp ide e atıyor.
        //sonra da ToStockDto objesine veriyorC
    }
}