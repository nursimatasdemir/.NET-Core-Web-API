using api.Data;
using api.DTOs.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> GetAll()
    {
        //We used .ToList method to deferred execution it is going to make all sql things for itself
        //.Select kullanmadan toStockDto a atama yapamayız .Select() ToStockDto a immutable bir list geri döndürür
        var stocks = await _context.Stock.ToListAsync();
        var stockDto = stocks.Select(s => s.ToStockDto());
        return Ok(stockDto);
        //we return everything on the datbase
    }
    
    //Her seferiinde tek kayıt almak için böyle tanımladık
    [HttpGet("{id}")]
    //We need to say exactly what are we returning id i o yüzden ekledik
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await _context.Stock.FindAsync(id); // id e göre kayıt döndürmek için bu metodu kullanıyoruz Find() or FirstOrDefault() 
        if (stock == null)
        {
            return NotFound(); //stok boş olduğunda geri dönüş yapmamız lazım
        }
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
    {
        var stockModel = stockDto.ToStockFromCreateDTO();
        await _context.Stock.AddAsync(stockModel);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        //bu kod şu anlama geliyor eğer ,den önceki kısım çalışırsa stockModelin idsini alıp ide e atıyor.
        //sonra da ToStockDto objesine veriyor 
        //ÇÜNKÜ VERİ TABANINA ID OLMADAN VERİ ATAYAMAZSIN. VERİNİN KULLANICIDAN IDSİNİ ALMIYORUZ AMA BUNU ARKADA BİZİM ATMAMIZ GEREKİYOR
    }

    [HttpPut]
    [Route("{id}")]
    //FromRoute ve FromBody kelimelerini birlikte kullanıyoruz. FromRoute id almamızı FromBody de Json şeklindeki veriyi almamızı sağlıyor
    //FromRoute ile id url üzerinden gönderiliyor sonra create çalıştırılıyor yeni eklenen veri için ilk önce ID oluşturmuş olduk içi boş şu an sonra 
    //create ile içerisine veri yazıcaz 
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
    {
        //veri var mı diye kontrol et
        var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
        if (stockModel == null)
        {
            return NotFound();//veriyi bulamazsa NotFound döndürecek
        } 
        stockModel.Symbol = updateDto.Symbol;
        stockModel.CompanyName = updateDto.CompanyName;
        stockModel.Purchase = updateDto.Purchase;
        stockModel.LastDiv = updateDto.LastDiv;
        stockModel.Industry = updateDto.Industry;
        stockModel.MarketCap = updateDto.MarketCap;
        //veriyi burada güncelliyoruz ve buradan sonra Entity Framework bunların takibini yapıyor değişiklikler üzeirne
        
        await _context.SaveChangesAsync();//bu komuttan sonra artık veritabanı güncelleniyor
        
        return Ok(stockModel.ToStockDto());//güncel halini de DTO olarak döndürmek istediğimzi için ona dönüştürdük
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
        if (stockModel == null)
        {
            return NotFound();
        }
        _context.Stock.Remove(stockModel);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}