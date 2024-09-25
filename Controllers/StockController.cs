using api.Data;
using api.DTOs.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Build.Framework;

namespace api.Controllers;

[Route("api/stock")]
//the following controller will upload everything from controller class
[ApiController]
//We specified this class as a Controller that was what we did first because it is easy to modify that way then we are going to add attributes
public class StockController : ControllerBase
{
    //Güvenlik açısından _context değişkenini private tanımladık her sınıf veritabanına erişemesin
    private readonly ApplicationDbContext _context;
    private readonly IStockRepository _stockRepo;
    public StockController(ApplicationDbContext context, IStockRepository stockRepo)
    {
        _context = context;
        _stockRepo = stockRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stocks = await _stockRepo.GetAllAsync(query);
        
        var stockDto = stocks.Select(s => s.ToStockDto());
        
        return Ok(stocks);
        //we return everything on the datbase
    }
    
    //Her seferiinde tek kayıt almak için böyle tanımladık
    [HttpGet("{id:int}")]
    //We need to say exactly what are we returning id i o yüzden ekledik
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var stock = await _stockRepo.GetByIdAsync(id); // id e göre kayıt döndürmek için bu metodu kullanıyoruz Find() or FirstOrDefault() 
        
        if (stock == null)
        {
            return NotFound(); //stok boş olduğunda geri dönüş yapmamız lazım
        }
        
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var stockModel = stockDto.ToStockFromCreateDTO();
        
        await _stockRepo.CreateAsync(stockModel);
        
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        //bu kod şu anlama geliyor eğer ,den önceki kısım çalışırsa stockModelin idsini alıp ide e atıyor.
        //sonra da ToStockDto objesine veriyor 
        //ÇÜNKÜ VERİ TABANINA ID OLMADAN VERİ ATAYAMAZSIN. VERİNİN KULLANICIDAN IDSİNİ ALMIYORUZ AMA BUNU ARKADA BİZİM ATMAMIZ GEREKİYOR
    }

    [HttpPut]
    [Route("{id:int}")]
    //FromRoute ve FromBody kelimelerini birlikte kullanıyoruz. FromRoute id almamızı FromBody de Json şeklindeki veriyi almamızı sağlıyor
    //FromRoute ile id url üzerinden gönderiliyor sonra create çalıştırılıyor yeni eklenen veri için ilk önce ID oluşturmuş olduk içi boş şu an sonra 
    //create ile içerisine veri yazıcaz 
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        //veri var mı diye kontrol et
        var stockModel = await _stockRepo.UpdateAsync(id, updateDto);
        
        if (stockModel == null)
        {
            return NotFound();//veriyi bulamazsa NotFound döndürecek
        } 
        
        return Ok(stockModel.ToStockDto());//güncel halini de DTO olarak döndürmek istediğimzi için ona dönüştürdük
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var stockModel = await _stockRepo.DeleteAsync(id);
        
        if (stockModel == null)
        {
            return NotFound();
        }
        
        return NoContent();
    }
}