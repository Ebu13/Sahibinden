use Sahibinden;
go


SELECT 
    U.username AS Kullanici,
    C.year AS Yil,
    M.name AS Araba_Modeli,
    C.price AS Fiyat
FROM 
    Cars C
JOIN 
    Users U ON C.user_id = U.user_id
JOIN 
    Menu M ON C.menu_id = M.menu_id
ORDER BY 
    C.price DESC;  -- Fiyat sýrasýna göre sýralama
