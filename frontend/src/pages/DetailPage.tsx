import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { Card, CardContent, Typography, Button, Stack } from '@mui/material';
import api from '../api';

type Property = {
  id: number;
  title: string;
  address: string;
  city: string;
  price: number;
  listingType: number;
  bedrooms: number;
  bathrooms: number;
  carSpots: number;
  description: string;
};

export default function DetailPage() {
  const { id } = useParams();
  const [p, setP] = useState<Property | null>(null);
  const [error, setError] = useState<string | null>(null);

  const load = async () => {
    try {
      const res = await api.get(`/properties/${id}`);
      setP(res.data);
    } catch (e: any) {
      setError('Failed to load');
    }
  };

  const toggleFavorite = async () => {
    await api.post(`/favorites/${id}`);
    alert('Toggled favorite');
  };

  useEffect(() => { load(); }, [id]);

  if (error) return <Typography color="error">{error}</Typography>;
  if (!p) return <Typography>Loading...</Typography>;

  return (
    <Card>
      <CardContent>
        <Stack spacing={1}>
          <Typography variant="h5">{p.title}</Typography>
          <Typography>{p.address}, {p.city}</Typography>
          <Typography variant="h6">${p.price.toLocaleString()} {p.listingType === 0 ? 'Rent' : 'Sale'}</Typography>
          <Typography>{p.bedrooms} bd · {p.bathrooms} ba · {p.carSpots} car</Typography>
          <Typography>{p.description}</Typography>
          <Button variant="contained" onClick={toggleFavorite}>Save/Unsave</Button>
        </Stack>
      </CardContent>
    </Card>
  );
}
