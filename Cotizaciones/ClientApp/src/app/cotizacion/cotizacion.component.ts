import { Component, Inject, OnInit, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IntervalObservable } from 'rxjs/observable/IntervalObservable';
import { takeWhile } from 'rxjs/operators';

@Component({
  selector: 'app-cotizacion',
  templateUrl: './cotizacion.component.html',
})

@Injectable()
export class CotizacionComponent implements OnInit {

  public cotizacion: Cotizacion[] = [];
  private baseUrl: string;
  private alive: boolean;
  private intervalObservableSub;
  
  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    console.log("ngOnInit");
    this.alive = true;

    // Inicialización
    this.httpClient.get<Cotizacion>(this.baseUrl + 'api/Cotizacion/dolar').subscribe(result => {
      console.log('Calling api dolar ok');
      result.fecha = new Date().toLocaleTimeString();
      this.cotizacion.push(result);
    }, error => console.error(error));
    this.httpClient.get<Cotizacion>(this.baseUrl + 'api/Cotizacion/euro').subscribe(result => {
      console.log('Calling api euro ok');
      result.fecha = new Date().toLocaleTimeString();
      this.cotizacion.push(result);
    }, error => console.error(error));
    this.httpClient.get<Cotizacion>(this.baseUrl + 'api/Cotizacion/real').subscribe(result => {
      console.log('Calling api real ok');
      result.fecha = new Date().toLocaleTimeString();
      this.cotizacion.push(result);
    }, error => console.error(error));

    this.intervalObservableSub = IntervalObservable.create(5000)
    .pipe(takeWhile(() => this.alive))
      .subscribe(() => {
        this.httpClient.get<Cotizacion>(this.baseUrl + 'api/Cotizacion/dolar').subscribe(result => {
          console.log('Calling api dolar ok');
          result.fecha = new Date().toLocaleTimeString();
          this.cotizacion[0] = result;
        }, error => console.error(error));
        this.httpClient.get<Cotizacion>(this.baseUrl + 'api/Cotizacion/euro').subscribe(result => {
          console.log('Calling api euro ok');
          result.fecha = new Date().toLocaleTimeString();
          this.cotizacion[1] = result;
        }, error => console.error(error));
        this.httpClient.get<Cotizacion>(this.baseUrl + 'api/Cotizacion/real').subscribe(result => {
          console.log('Calling api real ok');
          result.fecha = new Date().toLocaleTimeString();
          this.cotizacion[2] = result;
        }, error => console.error(error));
      });
  }

  ngOnDestroy() {
    console.log("ngOnDestroy");
    this.alive = false;
    this.intervalObservableSub.unsubscribe();
  }
}

interface Cotizacion {
  moneda: string;
  precio: string;
  fecha: string;
}
