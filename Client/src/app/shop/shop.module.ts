import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { MovieItemComponent } from './movie-item/movie-item.component';

@NgModule({
  declarations: [ShopComponent, MovieItemComponent],
  imports: [CommonModule],
  exports: [ShopComponent],
})
export class ShopModule {}
