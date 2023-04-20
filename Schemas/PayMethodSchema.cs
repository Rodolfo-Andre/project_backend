﻿using System.ComponentModel.DataAnnotations;

namespace project_backend.Schemas
{
    public class PayMethodPrincipal
    {
        [Required(ErrorMessage = "El campo 'Método de Pago' es requerido")]
        [MinLength(3, ErrorMessage = "El campo Método de Pago' debe tener una longitud mínima de 3 caracteres")]
        [MaxLength(50, ErrorMessage = "El campo 'Método de Pago' debe tener una longitud máxima de 50 caracteres")]
        public string Paymethod { get; set; }
    }

    public class PayMethodGet : PayMethodPrincipal
    {
        public int Id { get; set; }
    }
}
