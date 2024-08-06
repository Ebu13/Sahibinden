import React from 'react';
import { Container, Typography, Box } from '@mui/material';

const SupplierHome = () => {
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
                    Tedarikçi Ana Sayfası
                </Typography>
                <Typography variant="body1">
                    Tedarikçi paneline hoş geldiniz! Buradan ürünlerinizi yönetebilir ve siparişlerinizi görebilirsiniz.
                </Typography>
            </Box>
        </Container>
    );
};

export default SupplierHome;
