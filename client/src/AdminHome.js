import React from 'react';
import { Container, Typography, Box, Button } from '@mui/material';
import { useNavigate } from 'react-router-dom';

const AdminHome = () => {
    const navigate = useNavigate();

    const handleLogout = () => {
        localStorage.removeItem('token');
        navigate('/login');
    };

    return (
        <Container maxWidth="lg">
            <Box
                sx={{
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                    marginTop: 8,
                    padding: 3,
                    boxShadow: 3,
                    borderRadius: 2,
                    bgcolor: 'background.paper',
                }}
            >
                <Typography variant="h4" gutterBottom>
                    Yönetici Ana Sayfası
                </Typography>
                <Typography variant="body1" gutterBottom>
                    Yönetici paneline hoş geldiniz! Buradan kullanıcıları ve sistem ayarlarını yönetebilirsiniz.
                </Typography>
                <Button variant="contained" color="secondary" onClick={handleLogout}>
                    Çıkış
                </Button>
            </Box>
        </Container>
    );
};

export default AdminHome;
