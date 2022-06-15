namespace ControleMedicamentos.Dominio.ModuloPaciente
{
    public class Paciente : EntidadeBase<Paciente>
    {
        public Paciente(string nome, string cartaoSUS)
        {
            Nome = nome;
            CartaoSUS = cartaoSUS;
        }

        public Paciente()
        {



        }

        public string Nome { get; set; }
        public string CartaoSUS { get; set; }

        public override void Atualizar(Paciente registro)
        {
            


        }
    }
}
