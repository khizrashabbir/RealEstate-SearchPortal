import { BrowserRouter, Routes, Route, Link, Navigate } from 'react-router-dom';
import { Container, AppBar, Toolbar, Typography, Button, CssBaseline } from '@mui/material';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import SearchPage from './pages/SearchPage';
import DetailPage from './pages/DetailPage';
import FavoritesPage from './pages/FavoritesPage';
import type { ReactElement } from 'react';

function PrivateRoute({ children }: { children: ReactElement }) {
  const token = localStorage.getItem('token');
  return token ? children : <Navigate to="/login" replace />;
}

function App() {
  const token = localStorage.getItem('token');
  const onLogout = () => { localStorage.removeItem('token'); window.location.href = '/login'; };
  const theme = createTheme({ palette: { mode: 'light' } });

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <BrowserRouter>
        <AppBar position="static">
        <Toolbar>
          <Typography variant="h6" sx={{ flexGrow: 1 }}>
            <Button color="inherit" component={Link} to="/">Real Estate</Button>
          </Typography>
          {token ? (
            <>
              <Button color="inherit" component={Link} to="/favorites">Favorites</Button>
              <Button color="inherit" onClick={onLogout}>Logout</Button>
            </>
          ) : (
            <>
              <Button color="inherit" component={Link} to="/login">Login</Button>
              <Button color="inherit" component={Link} to="/register">Register</Button>
            </>
          )}
        </Toolbar>
        </AppBar>
        <Container maxWidth={false} sx={{ mt: 3 }}>
          <Routes>
            <Route path="/" element={<SearchPage />} />
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
            <Route path="/properties/:id" element={<DetailPage />} />
            <Route path="/favorites" element={<PrivateRoute><FavoritesPage /></PrivateRoute>} />
          </Routes>
        </Container>
      </BrowserRouter>
    </ThemeProvider>
  );
}

export default App;
