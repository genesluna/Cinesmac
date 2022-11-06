import { Session } from './Session';

export class Movie {
  id: string;
  title: string;
  description: string;
  director: string;
  genre: string;
  length: number;
  imdbScore: number;
  imageURL: string;
  is3D: boolean;
  isIMAX: boolean;
  sessions: Session[];
}
