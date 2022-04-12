import { Component, OnInit } from '@angular/core';
import { faPlusCircle, faMinusCircle, faTrash } from '@fortawesome/free-solid-svg-icons';
import { Observable } from 'rxjs';
import { IBasket } from '../shared/models/basket';
import { BasketService } from './basket.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit {
  faPlusCircle = faPlusCircle
  faMinusCircle = faMinusCircle
  faTrash = faTrash

  basket$: Observable<IBasket>;

  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
  }

}

