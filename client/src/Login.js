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
            // Giriş başarılı, kullanıcı bilgilerini saklayın (örneğin localStorage)
            localStorage.setItem('userId', response.data.userId); // Kullanıcı ID'sini sakla
            navigate('/home'); // Ana sayfaya yönlendir
        } catch (err) {
            setError('Giriş başarısız. Lütfen tekrar deneyin.'); // Hata mesajı göster
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
                    Giriş Yap
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
