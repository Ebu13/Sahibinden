import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom'; // Switch yerine Routes
import BuyerHome from './BuyerHome';
import OrderPage from './OrderPage';
import AdminHome from './AdminHome';
import SupplierHome from './SupplierHome';
import './index.css'; // Stil dosyası (isteğe bağlı)
import Login from './Login';

ReactDOM.render(
  <Router>
    <Routes>
    <Route path="/buyer" element={<BuyerHome />} />
    <Route path="/supplier" element={<SupplierHome />} />
    <Route path="/admin" element={<AdminHome />} />
    <Route path="/order" element={<OrderPage />} />
    <Route path="/login" element={<Login />} /> {/* Login bileşeni ekleniyor */}
    <Route path="/" element={<Login />} />
</Routes>
  </Router>,
  document.getElementById('root')
);