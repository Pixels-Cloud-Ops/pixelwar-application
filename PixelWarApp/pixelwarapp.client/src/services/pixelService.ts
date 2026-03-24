import axios from "axios";

export interface Pixel {
  x: number;
  y: number;
  color: string;
}

const API_URL = "https://localhost:7009/api/pixels";

export const getPixels = async (): Promise<Pixel[]> => {
  const response = await axios.get(API_URL);
  return response.data;
};

export const savePixel = async (pixel: Pixel): Promise<Pixel> => {
  const response = await axios.put(API_URL, pixel);
  return response.data;
};