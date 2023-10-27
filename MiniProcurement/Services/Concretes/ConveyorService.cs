using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using MiniProcurement.Data.Contexts;
using MiniProcurement.Data.Entities;
using MiniProcurement.Data.Enumerations;
using MiniProcurement.Exceptions;
using MiniProcurement.Resources.Localization;
using MiniProcurement.Services.Interfaces;

namespace MiniProcurement.Services.Concretes;

public class ConveyorService : IConveyorService
{
    private readonly ApplicationDbContext _context;
    private readonly IStringLocalizer<ExceptionLoc> _localizer;

    public ConveyorService(ApplicationDbContext context, IStringLocalizer<ExceptionLoc> localizer)
    {
        _context = context;
        _localizer = localizer;
    }

    /*
    public async Task AssignApprover(int documentId, int approverId)
    {
        var document = await _context.Documents.Include(d => d.ApproveList)
            .SingleOrDefaultAsync(d => d.Id == documentId);

        var approver = await _context.Users.SingleOrDefaultAsync(u => u.Id == approverId) ??
                       throw new NotFoundException(_localizer["UserNotFound"]);

        if (document.ApproveList.Any(u => u.Id == approverId))
        {
            throw new Exception("Approver already approves");
        }
        else
        {
            document.ApproveList.Add(approver);
        }

        await _context.SaveChangesAsync();
    }

    public async Task ApproveDocument(int documentId, User approver)
    {
        var document =
            await _context.Documents.Include(d => d.ApproveList)
                .SingleOrDefaultAsync(d => d.Id == documentId) ??
            throw new NotFoundException(_localizer["DocumentNotFound"]);

        document.DocumentStatus = DocumentStatus.Waiting;

        var index = document.ApproveList.FindIndex(u => u.Id == approver.Id);

        if (index == 0) approver.HasApprovedDoc = true;
        if (index > 0)
        {
            var previousApprover = document.ApproveList[index - 1];
            if (previousApprover.HasApprovedDoc == false)
            {
                throw new Exception(
                    "This user can't approve the document right now. Please check if authority " +
                    "before this user has approved the document.");
            }
            else
            {
                approver.HasApprovedDoc = true;
                if (index == document.ApproveList.Count - 1)
                {
                    document.DocumentStatus = DocumentStatus.Submit;
                }
            }
        }

        if (document.DocumentStatus == DocumentStatus.Submit)
        {
            throw new Exception("Document has already been approved");
        }

        if (!document.ApproveList.Any(u => u.Id == approver.Id))
        {
            throw new Exception("Document doesn't contain approving authority with this id");
        }

        await _context.SaveChangesAsync();
    }*/
}