namespace api.Models;

public class Comment
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    
    public string Content { get; set; } = string.Empty;
    
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    
    //Convention kullanarak iki tabloyu bağladığımızda Entity FrameWork ün bizim için tablo oluşturmasını istiyoruz
    public int? StockId { get; set; } //this is going to actually form the relationship within two tables
   
    public Stock? Stock { get; set; } //navigation property
    
}