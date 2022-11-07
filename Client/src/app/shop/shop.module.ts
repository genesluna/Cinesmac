import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { MovieItemComponent } from './movie-item/movie-item.component';
import { SharedModule } from '../shared/shared.module';
import { MovieDetailsComponent } from './movie-details/movie-details.component';

@NgModule({
  declarations: [ShopComponent, MovieItemComponent, MovieDetailsComponent],
  imports: [CommonModule, SharedModule, RouterModule],
  exports: [ShopComponent],
})
export class ShopModule {}
