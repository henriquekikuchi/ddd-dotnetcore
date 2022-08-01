using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VemDeZap.Domain.Entities;

namespace VemDeZap.Infra.Repositories.Map;

public class MapGrupo : IEntityTypeConfiguration<Grupo>
{
    public void Configure(EntityTypeBuilder<Grupo> builder)
    {
        builder.ToTable("Grupo");

        //Propriedades
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nome).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Nicho).IsRequired();

        //foreignKey
        builder.HasOne(x => x.Usuario).WithMany().HasForeignKey("idUsuario");
    }
}
