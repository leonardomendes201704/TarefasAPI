﻿using Microsoft.EntityFrameworkCore;
using TarefasAPI.Data;
using TarefasAPI.Models;
using TarefasAPI.Repositories;

public class TarefaRepository : ITarefaRepository
{
    private readonly AppDbContext _context;

    public TarefaRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Tarefa> GetAll()
    {
        return _context.Tarefas.ToList();
    }

    public Tarefa? GetById(int id)
    {
        return _context.Tarefas.Find(id);
    }

    public void Add(Tarefa tarefa)
    {
        _context.Tarefas.Add(tarefa);
        _context.SaveChanges();
    }

    public void Update(Tarefa tarefa)
    {
        var local = _context.Tarefas.Local.FirstOrDefault(t => t.Id == tarefa.Id);
        if (local != null)
        {
            _context.Entry(local).State = EntityState.Detached;
        }

        _context.Tarefas.Update(tarefa);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var tarefa = _context.Tarefas.Find(id);
        if (tarefa != null)
        {
            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();
        }
    }
}
