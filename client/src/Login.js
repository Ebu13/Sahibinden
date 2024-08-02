import React, { useState } from 'react';
import axios from 'axios';
import { Container, TextField, Button, Typography } from '@mui/material';
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
        <Container>
            <Typography variant="h4" gutterBottom>Giriş Yap</Typography>
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
            <Button variant="contained" color="primary" onClick={handleLogin}>
                Giriş Yap
            </Button>
        </Container>
    );
};

export default Login;
