﻿using BlogFilipe.DB.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogFilipe.DB.Mapeamentos
{
    public class DownloadConfig : EntityTypeConfiguration<Download>
    {
        public DownloadConfig()
        {
            ToTable("DOWNLOAD");

            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("IDDOWNLOAD")
                .IsRequired();

            Property(x => x.Ip)
                .HasColumnName("IP")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.DataHora)
                .HasColumnName("DATAHORA")
                .IsRequired();

            Property(x => x.IdArquivo)
                .HasColumnName("IDARQUIVO")
                .IsRequired();

            HasRequired(x => x.Arquivo)
                .WithMany()
                .HasForeignKey(x => x.IdArquivo);
        }
    }
}
