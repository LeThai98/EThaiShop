import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { CartService } from '../../core/services/cart.service';
import { CartItemComponent } from './cart-item/cart-item.component';
import { OrderSummaryComponent } from '../../shared/component/order-summary/order-summary.component';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CartItemComponent, OrderSummaryComponent],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss'
})
export class CartComponent {
  private router = inject(Router)
  cartService = inject(CartService);
  
  onAction() {
    this.router.navigateByUrl('/shop');
  }
}
