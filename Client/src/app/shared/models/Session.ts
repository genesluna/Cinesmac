import { ScreeningRoom } from './ScreeningRoom';

export class Session {
  id: string;
  starTime: number;
  endTime: number;
  basePrice: number;
  halfPrice: number;
  vipPrice: number;
  screeningRoom: ScreeningRoom;
}
