﻿using Core;
using Core.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class KeyResultService: IKeyResultService
    {
        private readonly IUnitOfWork _unitOfWork;

        public KeyResultService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<KeyResult> CreateKeyResult(KeyResult newKeyResult)
        {
            await _unitOfWork.KeyResults.AddAsync(newKeyResult);
            _unitOfWork.Commit();
            return newKeyResult;          
        }

        public async Task DeleteKeyResult(KeyResult keyResult)
        {
            _unitOfWork.KeyResults.Remove(keyResult);
            _unitOfWork.Commit();
        }

        public IQueryable<KeyResult> GetAllKeyResults()
        {
            return _unitOfWork.KeyResults.GetAllAsync();
        }

        public async Task<KeyResult> GetKeyResultById(int id)
        {
            return await _unitOfWork.KeyResults.GetByIdAsync(id);
        }

        public async Task UpdateKeyResult(KeyResult keyResultToBeUpdated, KeyResult keyResult)
        {
            keyResultToBeUpdated.ActualValue = keyResult.ActualValue;
            keyResultToBeUpdated.Status = keyResult.Status;
            _unitOfWork.Commit();
        }
    }
}