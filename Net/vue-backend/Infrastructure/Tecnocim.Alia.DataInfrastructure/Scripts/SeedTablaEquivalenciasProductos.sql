UPDATE EquivalenciasProductos SET Descripcion='Aval (Largo)', Subtipo='largo' WHERE Tipo='aval'
UPDATE EquivalenciasProductos SET Descripcion='Préstamo (Largo)', Subtipo='largo' WHERE Tipo='prestamo'
UPDATE EquivalenciasProductos SET Descripcion='Leasing (Largo)', Subtipo='largo' WHERE Tipo='leasing'
UPDATE EquivalenciasProductos SET Descripcion='Hipoteca (Largo)', Subtipo='largo' WHERE Tipo='hipoteca'
UPDATE EquivalenciasProductos SET Descripcion='Póliza (Póliza)', Subtipo='poliza' WHERE Tipo='poliza'
UPDATE EquivalenciasProductos SET Descripcion='Click (Póliza)', Subtipo='poliza' WHERE Tipo='click'
INSERT INTO EquivalenciasProductos VALUES ('confirming estandar', 'Confirming Estándar (Compras)', 'compras')
INSERT INTO EquivalenciasProductos VALUES ('confirming pronto pago', 'Confirming Pronto Pago (Compras)', 'compras')
INSERT INTO EquivalenciasProductos VALUES ('financiacion importaciones', 'Financiación Importaciones (Compras)', 'compras')
INSERT INTO EquivalenciasProductos VALUES ('credito documentario importaciones', 'Crédito Documentario Importaciones (Compras)', 'compras')
INSERT INTO EquivalenciasProductos VALUES ('descuento pagares', 'Descuento Pagarés (Ventas)', 'ventas')
INSERT INTO EquivalenciasProductos VALUES ('descuento pagares no orden', 'Descuento Pagarés No Orden (Ventas)', 'ventas')
INSERT INTO EquivalenciasProductos VALUES ('anticipo recibos', 'Anticipo Recibos (Ventas)', 'ventas')
INSERT INTO EquivalenciasProductos VALUES ('anticipo facturas', 'Anticipo Facturas (Ventas)', 'ventas')
INSERT INTO EquivalenciasProductos VALUES ('anticipo pagos domiciliados', 'Anticipo Pagos Domiciliados (Ventas)', 'ventas')
INSERT INTO EquivalenciasProductos VALUES ('credito documentario exportaciones', 'Crédito Documentario Exportaciones (Ventas)', 'ventas')
INSERT INTO EquivalenciasProductos VALUES ('factoring con recurso', 'Factoring Con Recurso (Ventas)', 'ventas')
INSERT INTO EquivalenciasProductos VALUES ('factoring sin recurso', 'Factoring Sin Recurso (Ventas)', 'ventas')
