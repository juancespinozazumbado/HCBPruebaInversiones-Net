create or replace NONEDITIONABLE PACKAGE        HCB_MANEJA_INVERSIONES AS 

  --ESCRITURA
  
  PROCEDURE AGREGAR_INVERSION( MontoInversion in number, TasaInteres double precision, PlazoMeses number, CuponesAnuales number);
  PROCEDURE INSERTAR_DETALLE(IdInversion in number, Año in number, Cupon in number, 
  Saldo in number, InteresGanado in number, SaldoCapitalizado in number );
  PROCEDURE INSERTAR_ENCABEZADO(IdInversion in number, InteresTotal in number, SaldoCapitalizado in number );
  
  PROCEDURE CALCULAR_CUPONES(IdInversion in  number, MensageError out  nvarchar2  );
  PROCEDURE ACTUALIZAR_ENCABEZADOS(IdInversion in number, MensageError out nvarchar2 );
  
  
  ---Para lectura
  
  PROCEDURE LISTAR_INVERSIONES(Inversiones OUT SYS_REFCURSOR);
  PROCEDURE LISTAR_DETALLES_INVERSION(IdInversion in number , Detalles OUT SYS_REFCURSOR);
  PROCEDURE LISTAR_ENCABEZADOS_INVERSION(IdInversion in number , Encabezados OUT SYS_REFCURSOR);
   PROCEDURE OBTENER_INVERSION(IdInversion in number , Inversion OUT SYS_REFCURSOR);



END HCB_MANEJA_INVERSIONES;