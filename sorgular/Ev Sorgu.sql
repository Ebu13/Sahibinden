use Sahibinden;
go

SELECT 
    U.username AS Kullanici,
    H.location AS Konum,
    H.size AS Boyut,
    H.price AS Fiyat,
    M.name AS Ev_Tipi
FROM 
    Homes H
JOIN 
    Users U ON H.user_id = U.user_id
JOIN 
    Menu M ON H.menu_id = M.menu_id
ORDER BY 
    H.price ASC;  -- Fiyata göre artan sýralama
