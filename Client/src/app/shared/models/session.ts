import { ScreeningRoom } from './screening-room';

export interface Session {
  id: string;
  starTime: number;
  endTime: number;
  basePrice: number;
  halfPrice: number;
  vipPrice: number;
  screeningRoom: ScreeningRoom;
}
