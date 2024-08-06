import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom'; // Switch yerine Routes
import App from './App';
import OrderPage from './OrderPage';
import './index.css'; // Stil dosyası (isteğe bağlı)
import Login from './Login';

ReactDOM.render(
  <Router>
    <Routes>
    <Route path="/home" element={<App />} />
    <Route path="/order" element={<OrderPage />} />
    <Route path="/login" element={<Login />} /> {/* Login bileşeni ekleniyor */}
    <Route path="/" element={<Login />} />
</Routes>
  </Router>,
  document.getElementById('root')
);