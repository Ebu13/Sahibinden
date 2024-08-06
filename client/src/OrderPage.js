import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Container, Typography, List, ListItem, ListItemText, CircularProgress, Paper, Box } from '@mui/material';

// Function to get authorization headers
const getAuthHeaders = () => {
  const token = localStorage.getItem('token');
  return {
    headers: {
      Authorization: token ? `Bearer ${token}` : '',
    },
  };
};

const OrderPage = () => {
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [userDetails, setUserDetails] = useState(null);

  useEffect(() => {
    const fetchOrders = async () => {
      try {
        const userId = localStorage.getItem('userId');
        const response = await axios.get(`https://localhost:7297/api/Orders/user/${userId}`, getAuthHeaders());
        const fetchedOrders = response.data.$values;
        setOrders(fetchedOrders); // Set orders to state

        // Fetch user details
        const user = await fetchUserDetails(userId);
        setUserDetails(user); // Set user details to state
      } catch (err) {
        setError(err); // Set error to state
      } finally {
        setLoading(false); // Close loading state
      }
    };

    fetchOrders();
  }, []);

  const fetchUserDetails = async (userId) => {
    try {
      const response = await axios.get(`https://localhost:7297/api/User/${userId}`, getAuthHeaders());
      return response.data; // Return user details
    } catch (err) {
      console.error("Kullanıcı bilgileri alınamadı:", err);
      return null; // Return null in case of error
    }
  };

  const fetchMenuDetails = async (menuId) => {
    try {
      const response = await axios.get(`https://localhost:7297/api/Menu/${menuId}`, getAuthHeaders());
      return response.data; // Return menu details
    } catch (err) {
      console.error("Menü bilgileri alınamadı:", err);
      return null; // Return null in case of error
    }
  };

  if (loading) return (
    <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
      <CircularProgress />
    </Box>
  );
  if (error) return <Typography color="error">Bir hata oluştu: {error.message}</Typography>;

  return (
    <Container maxWidth="md" sx={{ mt: 4 }}>
      <Typography variant="h4" gutterBottom>
        Siparişler
      </Typography>
      {userDetails && (
        <Typography variant="h6">
          Kullanıcı: {userDetails.username}, Email: {userDetails.email}
        </Typography>
      )}
      <Paper elevation={3} sx={{ padding: 3 }}>
        <List>
          {orders.map(order => (
            <OrderItem key={order.orderId} order={order} fetchMenuDetails={fetchMenuDetails} />
          ))}
        </List>
      </Paper>
    </Container>
  );
};

const OrderItem = ({ order, fetchMenuDetails }) => {
  const [menuDetails, setMenuDetails] = useState(null);
  const [loadingMenu, setLoadingMenu] = useState(true);

  useEffect(() => {
    const getMenuDetails = async () => {
      const menu = await fetchMenuDetails(order.menuId);
      setMenuDetails(menu);
      setLoadingMenu(false);
    };

    getMenuDetails();
  }, [order.menuId, fetchMenuDetails]);

  if (loadingMenu) return <ListItem><CircularProgress size={24} /></ListItem>;
  if (!menuDetails) return <ListItem><Typography color="error">Menü bulunamadı.</Typography></ListItem>;

  return (
    <ListItem>
      <ListItemText
        primary={`Ürün Türü: ${order.productType}`}
        secondary={
          <ul>
            <li>{`Menü: ${menuDetails.name}`}</li>
          </ul>
        }
      />
    </ListItem>
  );
};

export default OrderPage;