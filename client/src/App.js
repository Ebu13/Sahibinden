import React, { useEffect, useState, useCallback } from 'react';
import axios from 'axios';
import { Container, Typography, Button, Grid, Card, CardContent, CardMedia, AppBar, Toolbar } from '@mui/material';
const App = () => {
  const [menus, setMenus] = useState([]);
  const [parentId, setParentId] = useState(0);
  const [selectedMenuNames, setSelectedMenuNames] = useState([]);
  const [items, setItems] = useState([]);
  const [users, setUsers] = useState({});
  const [initialMenu, setInitialMenu] = useState(null);

  const fetchUsers = useCallback((userIds) => {
    const uniqueUserIds = [...new Set(userIds)];
    const userPromises = uniqueUserIds.map(userId =>
      axios.get(`https://localhost:7297/api/User/${userId}`)
    );

    Promise.all(userPromises)
      .then(responses => {
        const usersData = {};
        responses.forEach(response => {
          const user = response.data;
          usersData[user.userId] = user;
        });
        setUsers(usersData);
      })
      .catch(error => {
        console.error('Kullanıcı bilgileri alınırken hata oluştu:', error);
      });
  }, []);

  const fetchItems = useCallback((menuId) => {
    let url = '';
    if (initialMenu === 'Araba') {
      url = `https://localhost:7297/api/Car/menu/${menuId}`;
    } else if (initialMenu === 'Ev') {
      url = `https://localhost:7297/api/Home/menu/${menuId}`;
    }

    if (url) {
      axios.get(url)
        .then(response => {
          setItems(response.data.$values);
          const userIds = response.data.$values.map(item => item.userId);
          fetchUsers(userIds);
        })
        .catch(error => {
          console.error('API çağrısında hata oluştu:', error);
        });
    }
  }, [initialMenu, fetchUsers]);

  const fetchMenus = useCallback((parentId) => {
    axios.get(`https://localhost:7297/api/Menu/parent/${parentId}`)
      .then(response => {
        if (response.data.$values.length === 0) {
          console.log('Boş API yanıtı alındı. Daha fazla veri yok.');
          setMenus([]);
        } else {
          setMenus(response.data.$values);
        }
      })
      .catch(error => {
        console.error('API çağrısında hata oluştu:', error);
      });
  }, []);

  useEffect(() => {
    fetchMenus(parentId);
    if (selectedMenuNames.length > 0 && selectedMenuNames[selectedMenuNames.length - 1] === initialMenu) {
      // Ana kategori seçildiğinde tüm öğeleri getir
      if (initialMenu === 'Araba') {
        axios.get(`https://localhost:7297/api/Car`)
          .then(response => {
            setItems(response.data.$values);
            const userIds = response.data.$values.map(item => item.userId);
            fetchUsers(userIds);
          })
          .catch(error => {
            console.error('API çağrısında hata oluştu:', error);
          });
      } else if (initialMenu === 'Ev') {
        axios.get(`https://localhost:7297/api/Home`)
          .then(response => {
            setItems(response.data.$values);
            const userIds = response.data.$values.map(item => item.userId);
            fetchUsers(userIds);
          })
          .catch(error => {
            console.error('API çağrısında hata oluştu:', error);
          });
      }
    } else {
      // Alt kategori seçildiğinde ilgili öğeleri getir
      fetchItems(parentId);
    }
  }, [parentId, selectedMenuNames, initialMenu, fetchMenus, fetchItems, fetchUsers]);

  const handleButtonClick = (menu) => {
    if (!initialMenu) {
      setInitialMenu(menu.name);
    }
    setParentId(menu.menuId);
    setSelectedMenuNames(prevNames => [...prevNames, menu.name]);
    fetchItems(menu.menuId);
  };

  return (
    <Container>
      <AppBar position="static">
        <Toolbar>
          <Typography variant="h6" color="inherit">
            Menü Uygulaması
          </Typography>
        </Toolbar>
      </AppBar>
      <Typography variant="h4" gutterBottom>
        Menüler
      </Typography>
      <div>
        {selectedMenuNames.map((name, index) => (
          <Typography key={index} variant="h6">{name}</Typography>
        ))}
      </div>
      <div>
        {menus.map(menu => (
          <Button key={menu.menuId} variant="contained" color="primary" onClick={() => handleButtonClick(menu)}>
            {menu.name}
          </Button>
        ))}
      </div>
      <Grid container spacing={4} style={{ marginTop: '20px' }}>
        {items.map((item, index) => {
          const user = users[item.userId];
          return (
            <Grid item xs={12} sm={6} md={3} key={index}>
              <Card>
                <CardMedia
                  component="img"
                  height="140"
                  image={`photos/${initialMenu}/${item.photoPath}.jpg`}
                  alt={item.name || 'Ürün'}
                />
                <CardContent>
                  <Typography variant="h6" component="div">
                    {item.name || 'Ürün Adı'}
                  </Typography>
                  <Typography variant="body2" color="text.secondary">
                    {initialMenu === 'Ev' ? (
                      <>
                        <strong>Konum:</strong> {item.location} <br />
                        <strong>Boyut:</strong> {item.size} m² <br />
                        <strong>Fiyat:</strong> {item.price} TL <br />
                      </>
                    ) : initialMenu === 'Araba' ? (
                      <>
                        <strong>Yıl:</strong> {item.year} <br />
                        <strong>Fiyat:</strong> {item.price} TL <br />
                      </>
                    ) : (
                      'Bilgi yok.'
                    )}
                    {user ? (
                      <div>
                        <strong>Kullanıcı Adı:</strong> {user.username} <br />
                        <strong>Email:</strong> {user.email}
                      </div>
                    ) : (
                      <div>
                        <strong>Kullanıcı Bilgisi Bulunamadı</strong>
                      </div>
                    )}
                  </Typography>
                </CardContent>
              </Card>
            </Grid>
          );
        })}
      </Grid>
    </Container>
  );
}

export default App;
