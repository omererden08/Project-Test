# Project-Test
## Movement Control Notes

- Player hareketi, oluşturulan GameObject üzerine eklenen script ile `EventManager` kullanılarak kontrol edildi.
- Obje **active** durumdayken player hareketi kapatıldı.
- Obje **deactive** olduğunda player yeniden hareket edebilir hale getirildi.

## Random Color Change Notes

- Renk değiştirme işlemi için önce bir input tanımlandı.
- Input yakalandıktan sonra `EventManager` üzerinden ilgili fonksiyon çağrıldı.
- Rastgele renk seçimi için `ColorDatabase` oluşturuldu.
- Aynı rengin art arda seçilmemesi için `lastColorIndex` değişkeni kullanıldı.

## Final Color Change Flow

- `G` tuşuna basılır.
- `UserInput` bu girdiyi yakalar.
- `Player_ChangeColor_Request` eventi gönderilir.
- `ColorDatabase` bu eventi dinler.
- Önceki renkle aynı olmayacak şekilde rastgele bir `Color` seçilir.
- Seçilen renk, `Player_ChangeColor_Apply` eventi ile gönderilir.
- `Player` bu rengi alır.
- `SpriteRenderer.color` güncellenir.