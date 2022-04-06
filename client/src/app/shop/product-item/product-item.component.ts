import { Component, Input, OnInit } from '@angular/core';
import { IProduct } from '../../shared/models/product';
import { faCartShopping } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit {

  faCartShopping = faCartShopping
  @Input() product: IProduct;

  constructor() { }

  ngOnInit(): void {
    console.log("product: ", this.product);

  }

}
