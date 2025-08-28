import { useEffect, useState } from 'react';
import { Box, Card, CardContent, Typography, TextField, MenuItem, Button, Stack } from '@mui/material';
import { Link } from 'react-router-dom';
import api from '../api';

type ListingType = 0 | 1;

type Property = {
  id: number;
  title: string;
  address: string;
  city: string;
  price: number;
  listingType: ListingType;
  bedrooms: number;
  bathrooms: number;
  carSpots: number;
  description: string;
};

export default function SearchPage() {
  const [items, setItems] = useState<Property[]>([]);
  const [minPrice, setMinPrice] = useState<string>('');
  const [maxPrice, setMaxPrice] = useState<string>('');
  const [bedrooms, setBedrooms] = useState<string>('');
  const [city, setCity] = useState<string>('');
  const [listingType, setListingType] = useState<string>('');

  const load = async () => {
    const res = await api.get('/properties', { params: {
      minPrice: minPrice || undefined,
      maxPrice: maxPrice || undefined,
      bedrooms: bedrooms || undefined,
      city: city || undefined,
      listingType: listingType || undefined,
    }});
    setItems(res.data);
  };

  useEffect(() => { load(); }, []);

  return (
    <>
      <Stack direction={{ xs: 'column', sm: 'row' }} spacing={2} sx={{ mb: 2 }}>
        <TextField label="City" value={city} onChange={e=>setCity(e.target.value)} />
        <TextField label="Min Price" value={minPrice} onChange={e=>setMinPrice(e.target.value)} />
        <TextField label="Max Price" value={maxPrice} onChange={e=>setMaxPrice(e.target.value)} />
        <TextField label="Bedrooms" value={bedrooms} onChange={e=>setBedrooms(e.target.value)} />
        <TextField select label="Type" value={listingType} onChange={e=>setListingType(e.target.value)} sx={{ minWidth: 140 }}>
          <MenuItem value="">Any</MenuItem>
          <MenuItem value="0">Rent</MenuItem>
          <MenuItem value="1">Sale</MenuItem>
        </TextField>
        <Button variant="contained" onClick={load}>Search</Button>
      </Stack>

      <Box sx={{
        display: 'grid',
        gridTemplateColumns: { xs: '1fr', md: '1fr 1fr', lg: '1fr 1fr 1fr' },
        gap: 2,
      }}>
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
    </>
  );
}
