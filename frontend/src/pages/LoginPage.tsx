import { useState } from 'react';
import { TextField, Button, Paper, Typography, Stack } from '@mui/material';
import api from '../api';

export default function LoginPage() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState<string | null>(null);

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    try {
      const res = await api.post('/auth/login', { email, password });
      localStorage.setItem('token', res.data.token);
      window.location.href = '/';
    } catch (err: any) {
      setError(err?.response?.data || 'Login failed');
    }
  };

  return (
    <Paper sx={{ p: 3, width: '100%', maxWidth: 480, mx: 'auto', mt: 6 }}>
      <Typography variant="h5" gutterBottom>Login</Typography>
      <form onSubmit={onSubmit}>
        <Stack spacing={2}>
          <TextField
            label="Email"
            value={email}
            onChange={e => setEmail(e.target.value)}
            required
            type="email"
            autoComplete="email"
            fullWidth
            margin="normal"
            InputLabelProps={{ shrink: true }}
            sx={{ '& .MuiInputBase-root': { backgroundColor: '#fff' } }}
          />
          <TextField
            label="Password"
            value={password}
            onChange={e => setPassword(e.target.value)}
            required
            type="password"
            autoComplete="current-password"
            fullWidth
            margin="normal"
            InputLabelProps={{ shrink: true }}
            sx={{ '& .MuiInputBase-root': { backgroundColor: '#fff' } }}
          />
          {error && <Typography color="error">{error}</Typography>}
          <Button type="submit" variant="contained">Login</Button>
        </Stack>
      </form>
    </Paper>
  );
}
