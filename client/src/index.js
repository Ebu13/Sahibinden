import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom'; // Switch yerine Routes
import App from './App';
import OrderPage from './OrderPage';
import './index.css'; // Stil dosyası (isteğe bağlı)

ReactDOM.render(
  <Router>
    <Routes>
      <Route path="/" element={<App />} /> {/* App bileşeni element olarak geçildi */}
      <Route path="/order" element={<OrderPage />} /> {/* OrderPage bileşeni element olarak geçildi */}
    </Routes>
  </Router>,
  document.getElementById('root')
);
