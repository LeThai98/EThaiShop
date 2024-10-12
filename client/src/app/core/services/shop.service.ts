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
  types: string[] = [];
  brands: string[] = [];

  getProduct() {
    return this.http.get<Pagination<Product>>(this.baseUrl + '/product?pageSize=20');
  }

  getBrands() {
    if (this.brands.length > 0) return;
    return this.http.get<string[]>(this.baseUrl + '/product/brands').subscribe({
      next: response => this.brands = response,
    })
  }

  getTypes() {
    if (this.types.length > 0) return;
    return this.http.get<string[]>(this.baseUrl + '/product/types').subscribe({
      next: response => this.types = response,
    })
  }

}
