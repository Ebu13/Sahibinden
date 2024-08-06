import React, { useState } from 'react';
import axios from 'axios';
import { Container, TextField, Button, Typography, Box } from '@mui/material';
import { useNavigate } from 'react-router-dom';

const Login = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleLogin = async () => {
        try {
            const response = await axios.post('https://localhost:7297/api/User/login', { username, password });

            // Store JWT token and user details in localStorage
            localStorage.setItem('token', response.data.token);
            localStorage.setItem('userId', response.data.userId);
            localStorage.setItem('username', response.data.username);
            localStorage.setItem('email', response.data.email);
            localStorage.setItem('role', response.data.role);

            // Redirect based on user role
            if (response.data.role === 'Admin') {
                navigate('/admin');
            } else if (response.data.role === 'Supplier') {
                navigate('/supplier');
            } else if (response.data.role === 'Buyer') {
                navigate('/buyer');
            } else {
                navigate('/home');
            }
        } catch (err) {
            setError('Giriş başarısız. Lütfen tekrar deneyin.');
        }
    };

    return (
        <Container maxWidth="xs">
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
                    Giriş
                </Typography>
                {error && <Typography color="error">{error}</Typography>}
                <TextField
                    label="Kullanıcı Adı"
                    variant="outlined"
                    fullWidth
                    margin="normal"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                />
                <TextField
                    label="Şifre"
                    type="password"
                    variant="outlined"
                    fullWidth
                    margin="normal"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />
                <Button
                    variant="contained"
                    color="primary"
                    onClick={handleLogin}
                    sx={{ mt: 2 }}
                >
                    Giriş Yap
                </Button>
            </Box>
        </Container>
    );
};

export default Login;
