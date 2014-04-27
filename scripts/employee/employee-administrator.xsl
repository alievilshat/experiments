<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:table('sys_business')">
          <businessemployee>
            <businessemployeeid m:left="-153" m:top="161">pk:businessemployee(<xsl:value-of select="id" />-2)</businessemployeeid>
            <employeeid m:left="-28" m:top="179">2</employeeid>
            <businessid m:left="-154" m:top="196">
              <xsl:value-of select="id" />
            </businessid>
            <isdefault m:left="-31" m:top="214">1</isdefault>
          </businessemployee>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>