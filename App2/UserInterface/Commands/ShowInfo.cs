using App2.UserInterface.Commands.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace App2.UserInterface.Commands
{
    class ShowInfo : Command
    {
        public Task execute()
        {
            Console.WriteLine("��������� ��� ��� �������:\n'�������' - �������� ������� ��� ����� � ��������;\n" +
                "'�����' - �������� ��������� ����� ����� �� �������� �����/��������;\n" +
                "'��������' - �������� �������� ������������ ���� � �������;\n" +
                "'�������' - �������� ������� ������������ ���� �� ��������;\n" +
                "'�����' - �������� ������ ���������;\n");
            return Task.CompletedTask;
        }
        
    }
}