import { useEffect, useState } from 'react';
import { Box, Card, CardContent, Typography } from '@mui/material';
import { Link } from 'react-router-dom';
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

export default function FavoritesPage() {
  const [items, setItems] = useState<Property[]>([]);

  const load = async () => {
    const res = await api.get('/favorites');
    setItems(res.data);
  };

  useEffect(() => { load(); }, []);

  return (
    <Box sx={{ display: 'grid', gridTemplateColumns: { xs: '1fr', md: '1fr 1fr', lg: '1fr 1fr 1fr' }, gap: 2 }}>
      {items.map(p => (
        <Card key={p.id}>
          <CardContent>
            <Typography variant="h6" component={Link} to={`/properties/${p.id}`}>{p.title}</Typography>
            <Typography variant="body2">{p.address}, {p.city}</Typography>
            <Typography variant="subtitle1">${p.price.toLocaleString()} {p.listingType === 0 ? 'Rent' : 'Sale'}</Typography>
            <Typography variant="body2">{p.bedrooms} bd · {p.bathrooms} ba · {p.carSpots} car</Typography>
          </CardContent>
        </Card>
      ))}
    </Box>
  );
}
