import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Container, Typography, Button, List, ListItem, ListItemText, Paper, AppBar, Toolbar } from '@mui/material';

const App = () => {
  const [menus, setMenus] = useState([]);
  const [parentId, setParentId] = useState(0);
  const [selectedMenuNames, setSelectedMenuNames] = useState([]);
  const [items, setItems] = useState([]);
  const [users, setUsers] = useState({});
  const [initialMenu, setInitialMenu] = useState(null);

  const fetchMenus = (parentId) => {
    axios.get(`https://localhost:7297/api/Menus/parent/${parentId}`)
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
  };

  const fetchItems = (menuId) => {
    let url = '';
    if (initialMenu === 'Araba') {
      url = `https://localhost:7297/api/Cars/menu/${menuId}`;
    } else if (initialMenu === 'Ev') {
      url = `https://localhost:7297/api/Homes/menu/${menuId}`;
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
  };

  const fetchUsers = (userIds) => {
    const uniqueUserIds = [...new Set(userIds)];
    const userPromises = uniqueUserIds.map(userId =>
      axios.get(`https://localhost:7297/api/Users/${userId}`)
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
  };

  useEffect(() => {
    fetchMenus(parentId);
  }, [parentId]);

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
      <Paper style={{ marginTop: '20px', padding: '20px' }}>
        <Typography variant="h5">Gelen Veriler</Typography>
        <List>
          {items.map((item, index) => {
            const user = users[item.userId];
            return (
              <ListItem key={index}>
                {initialMenu === 'Ev' ? (
                  <ListItemText
                    primary={<strong>Konum:</strong>}
                    secondary={
                      <div>
                        {item.location} <br />
                        <strong>Boyut:</strong> {item.size} m² <br />
                        <strong>Fiyat:</strong> {item.price} TL <br />
                        {user && (
                          <div>
                            <strong>Kullanıcı Adı:</strong> {user.username} <br />
                            <strong>Email:</strong> {user.email}
                          </div>
                        )}
                      </div>
                    }
                  />
                ) : initialMenu === 'Araba' ? (
                  <ListItemText
                    primary={<strong>Yıl:</strong>}
                    secondary={
                      <div>
                        {item.year} <br />
                        <strong>Fiyat:</strong> {item.price} TL <br />
                        {user && (
                          <div>
                            <strong>Kullanıcı Adı:</strong> {user.username} <br />
                            <strong>Email:</strong> {user.email}
                          </div>
                        )}
                      </div>
                    }
                  />
                ) : (
                  <ListItemText primary="Bilgi yok." />
                )}
              </ListItem>
            );
          })}
        </List>
      </Paper>
    </Container>
  );
}

export default App;
