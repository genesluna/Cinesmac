export class BasketItem {
  id: string;
  sessionStartTime: number;
  ticketType: number;
  movieTitle: string;
  screeningRoomName: string;
  price: number;
  quantity: number = 1;
  imageUrl: string;
}
