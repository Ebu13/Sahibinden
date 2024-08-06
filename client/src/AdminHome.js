import React from 'react';
import { Container, Typography, Box } from '@mui/material';

const AdminHome = () => {
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
                <Typography variant="body1">
                    Yönetici paneline hoş geldiniz! Buradan kullanıcıları ve sistem ayarlarını yönetebilirsiniz.
                </Typography>
            </Box>
        </Container>
    );
};

export default AdminHome;
