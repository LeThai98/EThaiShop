import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Product } from '../../shared/models/product';
import { MatCard } from '@angular/material/card';
import { ProductItemComponent } from './product-item/product-item.component';
import { MatDialog } from '@angular/material/dialog';
import { FiltersDialogComponent } from './filters-dialog/filters-dialog.component';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { MatMenu, MatMenuModule, MatMenuTrigger } from '@angular/material/menu';
import { MatList, MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { MatSelectChange } from '@angular/material/select';
import { ShopParams } from '../../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [MatCard,
            ProductItemComponent, 
            MatIcon, 
            MatButton,
            MatMenuModule,
            MatSelectionList,
            MatListOption,
            MatMenuTrigger],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {
  private shopService = inject(ShopService);
  private dialogService = inject(MatDialog);
  products: Product[] = []; 
  shopParams = new ShopParams();

  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low-High', value: 'priceAsc'},
    {name: 'Price: High-Low', value: 'priceDesc'},
  ];

  ngOnInit() {
    this.iniializeShop();
  }

  iniializeShop() {
    this.shopService.getBrands();
    this.shopService.getTypes();
    this.getProduct();
  }

  openFilterDialog() { 
    const dialogRef = this.dialogService.open(FiltersDialogComponent, {
      minWidth: '500px',
      data: {selectedBrand: this.shopParams.brands, selectedType: this.shopParams.types}
    });

    dialogRef.afterClosed().subscribe({
      next: result => {
        this.shopParams.brands = result.selectedBrand;
        this.shopParams.types = result.selectedType;

        this.getProduct();
      }
    });
  }

  onSortChange(event: MatSelectionListChange) {
    const selectedOption = event.options[0];

    if(selectedOption) {
      this.shopParams.sort = selectedOption.value;
      this.getProduct();
    }
  }

  getProduct() {
    this.shopService.getProduct(this.shopParams).subscribe({
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

}

