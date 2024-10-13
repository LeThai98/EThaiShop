import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Product } from '../../shared/models/product';
import { MatCard } from '@angular/material/card';
import { ProductItemComponent } from './product-item/product-item.component';
import { MatDialog } from '@angular/material/dialog';
import { FiltersDialogComponent } from './filters-dialog/filters-dialog.component';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [MatCard,ProductItemComponent, MatIcon, MatButton],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {
  private shopService = inject(ShopService);
  private dialogService = inject(MatDialog);
  products: Product[] = []; 
  selectedBrand: string[] = [];
  selectedType: string[] = [];

  ngOnInit() {
    this.iniializeShop();
  }

  iniializeShop() {
    this.shopService.getBrands();
    this.shopService.getTypes();
    this.shopService.getProduct().subscribe({
      next: response => {
        this.products = response.data;
      },
      error: error => {
        console.error('There was an error!', error);
      },
      complete: () => {
      }
    });
  }

  openFilterDialog() { 
    const dialogRef = this.dialogService.open(FiltersDialogComponent, {
      minWidth: '500px',
      data: {selectedBrand: this.selectedBrand, selectedType: this.selectedType}
    });

    dialogRef.afterClosed().subscribe({
      next: result => {
        this.selectedBrand = result.selectedBrand;
        this.selectedType = result.selectedType;

        this.shopService.getProduct(this.selectedBrand, this.selectedType).subscribe({
          next: response => {
            this.products = response.data;
          },
          error: error => {
            console.error('There was an error!', error);
          },
          complete: () => {
          }
        });
      }
    });
  }
}
