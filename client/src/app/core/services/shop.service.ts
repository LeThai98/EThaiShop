import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Product } from '../../shared/models/product';
import { Pagination } from '../../shared/models/pagination';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api';
  private http = inject(HttpClient);

  getProduct() {
    return this.http.get<Pagination<Product>>(this.baseUrl + '/product?pageSize=20');
  }

}
