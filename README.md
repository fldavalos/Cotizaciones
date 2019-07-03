# Cotizaciones
 Obtiene la cotización en ARS desde USD, EUR y BRL

# Aclaraciones
 La aplicación está hecha en ASP.NET Core, con Front-End en Angular.
 
 Una vez ejecutada la aplicación, se mostrará en pantalla una tabla con la cotización en pesos argentinos (ARS) de las monedas Dólar Estadounidense (USD), Euro (EUR) y Real (BRL). 
 La frecuencia de actualización de la cotización es de 5 segundos.
 
 También se puede consumir independientemente (con Postman por ejemplo) la API que obtiene las cotizaciones de las monedas en cuestión con la siguiente nomenclatura:
 BASE_URL/api/cotizacion/dolar
 BASE_URL/api/cotizacion/euro
 BASE_URL/api/cotizacion/real
 
 NOTA: BASE_URL se debe reemplazar según el entorno donde se ejecute el programa, normalmente será localhost:PORT
  