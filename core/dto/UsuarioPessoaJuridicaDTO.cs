using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Energytrack.core.DTO
{
    public class UsuarioPessoaJuridicaDTO
    {
        private string _nome;
        private string _apelido;
        private string _serial;

        public UsuarioPessoaJuridicaDTO() { }
        public UsuarioPessoaJuridicaDTO(string nome, string apelido, string serial)
        {
            this._nome = nome;
            this._apelido = apelido;
            this._serial = serial;
        }

        public string GetApelido() => this._apelido;

        public string GetNome() => this._nome;

        public string GetSerial() => this._serial;

        public void SetNome(string value)
        {
            this._nome = value;
        }

        public void SetApelido(string value)
        {
            this._apelido = value;
        }

        public void SetSerial(string value)
        {
            this._serial = value;
        }

        public List<UsuarioPessoaJuridicaDTO> usuarioList() 
        {
            List<UsuarioPessoaJuridicaDTO> list = new List<UsuarioPessoaJuridicaDTO>();
            list.Add(new UsuarioPessoaJuridicaDTO(_nome, _apelido, _serial));
            return list;
        }

    }
}